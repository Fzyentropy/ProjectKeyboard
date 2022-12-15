using Unity.VisualScripting;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace Script.SceneObjects
{
    public class ScoreTracker
    {
        [SerializeField] private Billboard.Player ScorePlayer;
        [SerializeField] private int LevelIndex;
        [SerializeField] private MMF_Player goalFeedback;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                GameManager.Instance.OnScores(ScorePlayer, LevelIndex);
            }
        }
    }
}