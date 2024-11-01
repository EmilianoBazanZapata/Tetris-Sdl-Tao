using Domain.Interfaces;

namespace MyGame.Interfaces
{
    public interface IGameLogicService
    {
        (IPiece currentPiece, IPiece nextPiece) GenerateRandomPieces();
        void HoldPiece();
        void ClearCompleteRows();
        void MovePieceAutomatically();
    }
}