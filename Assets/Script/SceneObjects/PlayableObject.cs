using System;
using System.Collections;
// using ExternalPropertyAttributes;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.SceneObjects
{
    public class PlayableObject : MonoBehaviour
    {
        public int ObjectID;
        public bool resetRequired = true;
        [ShowIf("resetRequired")]
        public Vector3 spawnPosition;
        [ShowIf("resetRequired")]
        public float resetTime = 2f;
        public Billboard.Player player;

        private static bool IsWrestlingTriggered = false;

        private void Start()
        {
            if (ObjectID == 0) IsWrestlingTriggered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("LeftScoreField"))
            {
                print("right goal");
                GameManager.Instance.OnScores(Billboard.Player.Right, ObjectID);
                if (ObjectID == 4) StartCoroutine(ResetPosition());
            }
            else if (other.tag.Equals("RightScoreField"))
            {
                print("left goal");
                GameManager.Instance.OnScores(Billboard.Player.Left, ObjectID);
                if (ObjectID == 4) StartCoroutine(ResetPosition());
            }
            else if (other.tag.Equals("SailingScoreField"))
            {
                print("sailing winner: " + player);
                GameManager.Instance.OnWins(player, ObjectID);
            }
            else if (other.tag.Equals("WrestlingScoreField"))
            {
                if (IsWrestlingTriggered) return;
                print("ring loser: "+ player);
                Billboard.Player winnerPlayer = Billboard.Player.Left;
                if (player.Equals(Billboard.Player.Left)) winnerPlayer = Billboard.Player.Right;
                else if (player.Equals(Billboard.Player.Right)) winnerPlayer = Billboard.Player.Left;
                GameManager.Instance.OnWins(winnerPlayer, ObjectID);
                IsWrestlingTriggered = true;
            }
            else if (other.tag.Equals("FootballResetField"))
            {
                StartCoroutine(ResetPosition());
            }
        }

        private IEnumerator ResetPosition()
        {
            print("reset");
            yield return new WaitForSeconds(resetTime);
            transform.position = spawnPosition;
        }
    }
}