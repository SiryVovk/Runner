using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseSystem : MonoBehaviour
{
    private DatabaseReference _dbRef;

    private const string DatabaseUrl = "https://runnergamefire-default-rtdb.europe-west1.firebasedatabase.app/";
    private const string LeaderboardPath = "Leaderboard";
    private const string ScorePath = "score";
    private const string UsernamePath = "username";

    private void Start()
    {
        _dbRef = FirebaseDatabase.GetInstance(DatabaseUrl).RootReference;
    }

    public IEnumerator AddUserToLeaderboardRoutine(string userId, string userName)
    {
        var userData = new Dictionary<string, object>
        {
            { UsernamePath, userName },
            { ScorePath, 0 }
        };

        var dbTask = _dbRef.Child(LeaderboardPath).Child(userId).SetValueAsync(userData);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
            Debug.LogError($"Failed to add user: {dbTask.Exception}");
    }

    public void UpdateUserScore(int newScore)
    {
        var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;
        if (currentUser == null) return;

        string userId = currentUser.UserId;
        DatabaseReference scoreRef = _dbRef.Child(LeaderboardPath).Child(userId).Child(ScorePath);

        scoreRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || !task.Result.Exists) return;

            int currentHighScore = Convert.ToInt32(task.Result.Value);

            if (newScore > currentHighScore)
            {
                scoreRef.SetValueAsync(newScore);
            }
        });
    }

    public void GetTopHighscores(int numberToGet, Action<List<UserData>> callback)
    {
        _dbRef.Child(LeaderboardPath).OrderByChild(ScorePath).LimitToLast(numberToGet)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    callback?.Invoke(null);
                    return;
                }

                var leaderboardList = new List<UserData>();
                foreach (var childSnapshot in task.Result.Children)
                {
                    var userDict = childSnapshot.Value as Dictionary<string, object>;
                    leaderboardList.Add(new UserData(
                        childSnapshot.Key,
                        userDict[UsernamePath].ToString(),
                        Convert.ToInt32(userDict[ScorePath])
                    ));
                }

                leaderboardList.Reverse();
                callback?.Invoke(leaderboardList);
            });
    }

    public void GetCurrentUserStats(Action<UserData> callback)
    {
        var user = FirebaseAuth.DefaultInstance.CurrentUser;

        if (user == null)
        {
            Debug.LogWarning("Attempted to get stats, but no user is logged in.");
            callback?.Invoke(null);
            return;
        }

        _dbRef.Child(LeaderboardPath).Child(user.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || !task.Result.Exists)
            {
                Debug.LogWarning($"Could not find stats for user: {user.UserId}");
                callback?.Invoke(null);
                return;
            }

            var userDict = task.Result.Value as Dictionary<string, object>;

            if (userDict == null)
            {
                callback?.Invoke(null);
                return;
            }

            UserData data = new UserData(
                user.UserId,
                userDict.ContainsKey(UsernamePath) ? userDict[UsernamePath].ToString() : "Unknown",
                userDict.ContainsKey(ScorePath) ? Convert.ToInt32(userDict[ScorePath]) : 0
            );

            callback?.Invoke(data);
        });
    }
}

[System.Serializable]
public class UserData
{
    public string UserId { get; private set; }
    public string Username { get; private set; }
    public int Score { get; private set; }

    public UserData(string userId, string username, int score)
    {
        UserId = userId;
        Username = username;
        Score = score;
    }
}