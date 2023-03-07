using UnityEngine.UI;

namespace GameState
{
    public abstract class GameState
     {
         protected readonly Image PredictionPointImage;

         protected GameState(Image predictionPoint)
         {
             PredictionPointImage = predictionPoint;
         }
     
         public abstract void Start();
         public abstract void SetCursor();
     }
}
