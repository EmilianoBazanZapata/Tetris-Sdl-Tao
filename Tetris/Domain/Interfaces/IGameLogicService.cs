using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGameLogicService
    {
        (Piece currentPiece, Piece nextPiece) GenerateRandomPieces();
        void HoldPiece();
        void ClearCompleteRows();
        void FixPieceOnBoard(Piece piece, int totalBoardColumns, int totalBoardRows, int[,] board);
    }
}