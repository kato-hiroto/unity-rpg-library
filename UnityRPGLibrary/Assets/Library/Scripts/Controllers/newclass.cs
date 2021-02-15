// namespace UnityRPGLibrary.Assets.Library.Scripts.Controllers
// {
//     public class newclass
//     {
        
//     }
// }
//     // 下(正面)から時計回りに対応付け
//     public Sprite GetSprite(int dir, int phase)
//     {
//         if (mappingPattern == MappingPattern.Walking8d)
//         {
//             int imgDir = new int[8]{0, 1, 2, 5, 6, 7, 4, 3}[dir];
//             int imgPhase = new int[4]{1, 0, 1, 2}[phase];
//             return images[imgDir * 3 + imgPhase];
//         }
//         else
//         {
//             int dirCount = images.Count / phaseCount;
//             int imgDir = dirCount > 0 ? dir % dirCount : 0;
//             int imgPhase = phase % phaseCount;
//             return images[imgDir * phaseCount + imgPhase];
//         }
//     }