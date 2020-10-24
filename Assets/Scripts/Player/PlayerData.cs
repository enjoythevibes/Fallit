using System.Linq;
using enjoythevibes.Serialization;

namespace enjoythevibes.Player.Data
{
    public class PlayerData
    {
        public float timeRecord;

        public void SetEmpty()
        {
            timeRecord = 0;
        }

        public byte[] GetBytesData()
        {
            var types = new int[]
            {
                sizeof(int), // timeRecord
            };
            var size = types.Sum();
            var bytes = new byte[size];

            var offset = 0;
            DataConverter.SetBytes(bytes, timeRecord, ref offset);
            return bytes;
        }

        public void ReadFromBytes(byte[] bytes)
        {
            var offset = 0;
            var timeRecordData = DataConverter.ReadFloatBytes(bytes, ref offset);
            if (timeRecordData.HasValue)
                timeRecord = timeRecordData.Value;
            else
                timeRecord = 0;
        }
    }
}