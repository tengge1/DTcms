using System;

namespace DTcms.Model
{
    /// <summary>
    /// 频道字段表
    /// </summary>
    [Serializable]
    public partial class site_channel_field
    {
        public site_channel_field()
        { }
        #region Model
        private int _id;
        private int _channel_id = 0;
        private int _field_id = 0;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 频道ID
        /// </summary>
        public int channel_id
        {
            set { _channel_id = value; }
            get { return _channel_id; }
        }
        /// <summary>
        /// 字段ID
        /// </summary>
        public int field_id
        {
            set { _field_id = value; }
            get { return _field_id; }
        }
        #endregion
    }
}