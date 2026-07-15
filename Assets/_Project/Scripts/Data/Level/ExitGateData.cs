using Data.Core;

namespace Data.Level
{
    [System.Serializable]
    public class ExitGateData
    {
        public string id;
        public ColorId colorId;
        public ExitSide side;
        public int startIndex;
        public int length = 1;
    }
}