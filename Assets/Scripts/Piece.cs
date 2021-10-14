using JigsawPuzzle.Manager;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JigsawPuzzle
{
    public class Piece : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Vector3 rightPos;

        [Header("Neighbours")]
        [SerializeField] private Piece neighbourLeft;
        [SerializeField] private Piece neighbourRight;
        [SerializeField] private Piece neighbourTop;
        [SerializeField] private Piece neighbourBottom;
        [SerializeField] private bool placedInRightPos;

        private ObjectManipulator _objectManipulator;
        private PuzzleManager _puzzleManager;
        private SpriteRenderer _spriteRenderer;
        private const byte RightPosLayerOrder = 1;
        private const byte NotRightPosLayerOrder = 2;

        #endregion

        #region Properties

        public Piece NeighbourLeft => neighbourLeft;
        public Piece NeighbourRight => neighbourRight;
        public Piece NeighbourTop => neighbourTop;
        public Piece NeighbourBottom => neighbourBottom;

        #endregion
    
        #region UnityCallbacks

        private void Start()
        {
            rightPos = transform.position;
            _objectManipulator = GetComponent<ObjectManipulator>();
            _puzzleManager = FindObjectOfType<PuzzleManager>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            PlaceInRandomPos();
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, rightPos) < .1f && !placedInRightPos)
            {
                transform.position = rightPos;
                placedInRightPos = true;
                _puzzleManager.OnRightPos();
                _spriteRenderer.sortingOrder = RightPosLayerOrder;
                _puzzleManager.PlaceNeighbours(this);
            }

            if (placedInRightPos)
                transform.position = rightPos;
        }
    
        #endregion
    
        #region PublicMethods

        public void PlaceInRandomPos()
        {
            var pos = transform.localPosition;
            placedInRightPos = false;
            transform.localPosition = new Vector3(Random.Range(-20, -10), Random.Range(-5, 6), pos.z);
            _objectManipulator.enabled = true;
            _puzzleManager.ResetRightPosCount();
            _spriteRenderer.sortingOrder = NotRightPosLayerOrder;
        }

        public void GuidForNext(Vector3 pos)
        {
            if (placedInRightPos)
                return;
            transform.localPosition = pos;
        }

        public void MoveSomeWhere()
        {
            var pos = transform.localPosition;
            transform.localPosition = new Vector3(Random.Range(-20, -10), Random.Range(-5, 6), pos.z);
        }

        #endregion
    }
}
