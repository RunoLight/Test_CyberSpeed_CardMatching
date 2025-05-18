using System.Collections.Generic;
using Domain.Interfaces;

namespace Application
{
    public class MatchCheckService : IMatchCheckService
    {
        private readonly IGameStateService _state;
        private readonly ISoundPlayer _sound;

        public MatchCheckService(IGameStateService state, ISoundPlayer sound)
        {
            _state = state;
            _sound = sound;
        }

        public void TryMatch()
        {
            var faceUp = _state.GetFaceUpCards();
            if (faceUp.Count != 2)
                return;

            if (faceUp[0].PairId == faceUp[1].PairId)
            {
                _state.MatchCards(new List<int> { faceUp[0].Id, faceUp[1].Id });
                _sound.Play(SoundType.Match);
            }
            else
            {
                _state.ResetUnmatchedCards();
                _sound.Play(SoundType.Mismatch);
            }
        }
    }
}