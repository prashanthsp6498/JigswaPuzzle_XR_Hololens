using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JigsawPuzzle.Manager
{
    public class PuzzleManager : MonoBehaviour
    {
        
        #region Variables
        
        [SerializeField] private GameObject tileParent;

        [SerializeField] private Vector3 firstNeighbourPos;
        [SerializeField] private Vector3 secondNeighbourPos;
        [SerializeField] private Vector3 thirdNeighbourPos;
        [SerializeField] private Vector3 fourthNeighbourPos;
        
        private List<Piece> _pieces;
        private int _totalTileInRightPos;
        
        #endregion

        #region UnityCallbacks
        
        private void Start()
        {
            _pieces = tileParent.GetComponentsInChildren<Piece>().ToList();
            firstNeighbourPos = new Vector3(-7, 4, 0);
            secondNeighbourPos = new Vector3(-7, 1, 0);
            thirdNeighbourPos = new Vector3(-7, -2, 0);
            fourthNeighbourPos = new Vector3(-7, -5, 0);
        }
        
        #endregion
        
        #region PublicMethods

        public void OnRightPos()
        {
            _totalTileInRightPos++;
            if (_totalTileInRightPos == 36)
                Debug.Log("[WinState] ::: Completed");
        }

        public void ResetRightPosCount() => _totalTileInRightPos = 0;

        [ContextMenu("Shuffle")]
        public void Shuffle()
        {
            Debug.Log("[Manager] ::: Shuffling");
            _pieces.ForEach(piece => piece.PlaceInRandomPos());
        }

        public void PlaceNeighbours(Piece piece)
        {
            _pieces.ForEach(p=> p.MoveSomeWhere());
            if (piece.NeighbourTop)
                piece.NeighbourTop.GuidForNext(firstNeighbourPos);
            if (piece.NeighbourBottom)
                piece.NeighbourBottom.GuidForNext(secondNeighbourPos);
            if (piece.NeighbourLeft)
                piece.NeighbourLeft.GuidForNext(thirdNeighbourPos);
            if (piece.NeighbourRight)
                piece.NeighbourRight.GuidForNext(fourthNeighbourPos);
        }
        
        #endregion
    }
}