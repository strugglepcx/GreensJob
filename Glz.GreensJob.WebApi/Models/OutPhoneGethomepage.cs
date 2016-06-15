using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutPhoneGethomepage : ResultBase
    {
        private static readonly List<PhoneHomeCategory> _homeCategories = new List<PhoneHomeCategory>();

        /// <summary>
        /// 首页分类信息
        /// </summary>
        public static List<PhoneHomeCategory> HomeCategories
        {
            get { return _homeCategories; }
        }

        static OutPhoneGethomepage()
        {

        }

        public OutPhoneGethomepage()
        {

        }
        /// <summary>
        /// 折扣前缀文字
        /// </summary>
        [DataMember]
        public string discountprefix { get; set; }

        /// <summary>
        /// 折扣后缀文字
        /// </summary>
        [DataMember]
        public string discountsuffix { get; set; }

        /// <summary>
        /// 特价文字
        /// </summary>
        [DataMember]
        public string specialwords { get; set; }

        /// <summary>
        /// 满减文字
        /// </summary>
        [DataMember]
        public string fulltextreduction { get; set; }
        //[DataMember]
        //public int listCount { get; set; }
        //[DataMember]
        //public int addNum { get; set; }
        //[DataMember]
        //public string withGiftId { get; set; }
        //[DataMember]
        //public string withGiftText { get; set; }
        ///// <summary>
        ///// 限时抢购 
        ///// </summary>
        //[DataMember]
        //public string withGiftImg { get; set; }
        //[DataMember]
        //public string desenoId { get; set; }
        //[DataMember]
        //public string desenoText { get; set; }
        ///// <summary>
        ///// 套餐
        ///// </summary>
        //[DataMember]
        //public string desenoImg { get; set; }
        //[DataMember]
        //public string referralsId { get; set; }
        //[DataMember]
        //public string referralsText { get; set; }
        ///// <summary>
        ///// 今日推荐
        ///// </summary>
        //[DataMember]
        //public string referralsImg { get; set; }
        //[DataMember]
        //public int catcount { get; set; }

        /// <summary>
        /// 广告
        /// </summary>
        [DataMember]
        public List<PhoneAdvertInfo> bannersList { get; set; }
        ///// <summary>
        ///// 满赠
        ///// </summary>
        //[DataMember]
        //public PhoneAdvertInfo withGiftList { get; set; }
        ///// <summary>
        ///// 限时
        ///// </summary>
        //[DataMember]
        //public PhoneAdvertInfo desenoList { get; set; }

        /// <summary>
        /// 推荐
        /// </summary>
        [DataMember]
        public List<PhoneAdvertInfo> referralsList { get; set; }
        public string aaa { set; get; }
        ///// <summary>
        ///// 广告
        ///// </summary>
        //[DataMember]
        //public PhoneReferrals phoneReferrals { get; set; }


        ///// <summary>
        ///// 热门
        ///// </summary>
        //[DataMember]
        //public List<PhoneAdvertInfo> hotList { get; set; }
        /// <summary>
        /// 首页分类
        /// </summary>
        [DataMember]
        public List<PhoneHomeCategory> homeCategories { get; set; }

    }

    [DataContract]
    public class PhoneHomeCategory
    {
        [DataMember]
        public int cateid { set; get; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class PhoneGethomepage
    {
        [DataMember]
        public int listCount { get; set; }
        [DataMember]
        public int addNum { get; set; }
        [DataMember]
        public string withGiftId { get; set; }
        [DataMember]
        public string withGiftText { get; set; }
        [DataMember]
        public string withGiftImg { get; set; }
        [DataMember]
        public string desenoId { get; set; }
        [DataMember]
        public string desenoText { get; set; }
        [DataMember]
        public string desenoImg { get; set; }
        [DataMember]
        public string referralsId { get; set; }
        [DataMember]
        public string referralsText { get; set; }
        [DataMember]
        public string referralsImg { get; set; }
    }

    [DataContract]
    public class OutPhoneAdvertsList : ResultBase
    {
        public OutPhoneAdvertsList()
        {
            listCount = 0;
            addNum = 10;
            bannersList = new List<PhoneAdvertInfo>();
        }
        [DataMember]
        public int listCount { get; set; }
        [DataMember]
        public int addNum { get; set; }

        /// <summary>
        /// 广告
        /// </summary>
        [DataMember]
        public List<PhoneAdvertInfo> bannersList { get; set; }


    }

    [DataContract]
    /// <summary>
    /// 广告信息类
    /// </summary>
    public class PhoneAdvertInfo
    {

        private int _adposid;//广告位置id
        private int _type;//类型(0代表文字，1代表图片，2代表flash，3代表代码)
        private string _title;//标题
        private string _url;//网址
        private string _body;//主体
        private string _extfield1;//扩展字段1
        private string _extfield2;//扩展字段2
        private string _extfield3;//扩展字段3
        private string _extfield4;//扩展字段4
        private string _extfield5;//扩展字段5

        [DataMember]
        /// <summary>
        /// 广告位置id
        /// </summary>
        public int adPosId
        {
            get { return _adposid; }
            set { _adposid = value; }
        }
        [DataMember]
        /// <summary>
        /// 类型(0代表文字，1代表图片，2代表flash，3代表代码)
        /// </summary>
        public int type
        {
            get { return _type; }
            set { _type = value; }
        }
        [DataMember]
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        [DataMember]
        /// <summary>
        /// 网址
        /// </summary>
        public string url
        {
            get { return _url; }
            set { _url = value; }
        }
        [DataMember]
        /// <summary>
        /// 主体
        /// </summary>
        public string body
        {
            get { return _body; }
            set { _body = value; }
        }
        [DataMember]
        /// <summary>
        /// 扩展字段1
        /// </summary>
        public string extField1
        {
            get { return _extfield1; }
            set { _extfield1 = value; }
        }
        [DataMember]
        /// <summary>
        /// 扩展字段2
        /// </summary>
        public string extField2
        {
            get { return _extfield2; }
            set { _extfield2 = value; }
        }
        [DataMember]
        /// <summary>
        /// 扩展字段3
        /// </summary>
        public string extField3
        {
            get { return _extfield3; }
            set { _extfield3 = value; }
        }
        [DataMember]
        /// <summary>
        /// 扩展字段4
        /// </summary>
        public string extField4
        {
            get { return _extfield4; }
            set { _extfield4 = value; }
        }
        [DataMember]
        /// <summary>
        /// 扩展字段5
        /// </summary>
        public string extField5
        {
            get { return _extfield5; }
            set { _extfield5 = value; }
        }
    }


    [DataContract]
    public class OutPhoneReferralsList : ResultBase
    {
        public OutPhoneReferralsList()
        {
            listCount = 0;
            addNum = 10;
            bannersList = new List<PhoneReferrals>();
        }
        [DataMember]
        public int listCount { get; set; }
        [DataMember]
        public int addNum { get; set; }

        /// <summary>
        /// 广告
        /// </summary>
        [DataMember]
        public List<PhoneReferrals> bannersList { get; set; }
    }
    [DataContract]
    /// <summary>
    /// 单品促销活动信息类
    /// </summary>
    public class PhoneReferrals
    {
        private int _pmid;//促销活动id
        private int _pid;//商品id
        private DateTime _starttime1;//开始时间1
        private DateTime _endtime1;//结束时间1
        private int _userranklower;//用户等级下限
        private string _name;//活动名称
        private string _slogan;//活动广告语
        private int _discounttype;//折扣类型,0代表折扣，1代表直降，2代表折后价
        private int _discountvalue;//折扣值

        private int _isstock;//是否限制库存
        private int _stock;//库存
        private int _allowbuycount;//最大购买数量
        private string _pname;//商品名称
        private string _pstate;//商品状态
        private string _pshopprice;//商品商城价格

        //private int _coupontypeid;//优惠劵类型id
        //private int _paycredits;//支付积分
        [DataMember]
        /// <summary>
        /// 促销活动id
        /// </summary>
        public int PmId
        {
            get { return _pmid; }
            set { _pmid = value; }
        }
        [DataMember]
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        [DataMember]
        /// <summary>
        /// 开始时间1
        /// </summary>
        public DateTime StartTime1
        {
            get { return _starttime1; }
            set { _starttime1 = value; }
        }
        [DataMember]
        /// <summary>
        /// 结束时间1
        /// </summary>
        public DateTime EndTime1
        {
            get { return _endtime1; }
            set { _endtime1 = value; }
        }
        [DataMember]
        /// <summary>
        /// 用户等级下限
        /// </summary>
        public int UserRankLower
        {
            get { return _userranklower; }
            set { _userranklower = value; }
        }
        [DataMember]
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [DataMember]
        /// <summary>
        /// 活动广告语
        /// </summary>
        public string Slogan
        {
            get { return _slogan; }
            set { _slogan = value; }
        }
        [DataMember]
        /// <summary>
        /// 折扣类型,0代表折扣，1代表直降，2代表折后价
        /// </summary>
        public int DiscountType
        {
            get { return _discounttype; }
            set { _discounttype = value; }
        }
        [DataMember]
        /// <summary>
        /// 折扣值
        /// </summary>
        public int DiscountValue
        {
            get { return _discountvalue; }
            set { _discountvalue = value; }
        }
        //[DataMember]
        ///// <summary>
        ///// 优惠劵类型id
        ///// </summary>
        //public int CouponTypeId
        //{
        //    get { return _coupontypeid; }
        //    set { _coupontypeid = value; }
        //}
        //[DataMember]
        ///// <summary>
        ///// 支付积分
        ///// </summary>
        //public int PayCredits
        //{
        //    get { return _paycredits; }
        //    set { _paycredits = value; }
        //}
        [DataMember]
        /// <summary>
        /// 是否限制库存
        /// </summary>
        public int IsStock
        {
            get { return _isstock; }
            set { _isstock = value; }
        }
        [DataMember]
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
        [DataMember]
        /// <summary>
        /// 最大购买数量
        /// </summary>
        public int AllowBuyCount
        {
            get { return _allowbuycount; }
            set { _allowbuycount = value; }
        }
        [DataMember]
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Pname
        {
            get { return _pname; }
            set { _pname = value; }
        }
        [DataMember]
        /// <summary>
        /// 商品状态
        /// </summary>
        public string Pstate
        {
            get { return _pstate; }
            set { _pstate = value; }
        }
        [DataMember]
        /// <summary>
        /// 商品商城价格
        /// </summary>
        public string Pshopprice
        {
            get { return _pshopprice; }
            set { _pshopprice = value; }
        }

        /// <summary>
        /// 折后价
        /// </summary>
        public decimal Discountprice
        {
            set { _discountprice = value; }
            get
            {
                decimal _discountprice = Convert.ToDecimal(_pshopprice);
                switch (_discounttype)
                {
                    case 0:
                        _discountprice = _discountprice * _discountvalue / (decimal)10.00;
                        break;
                    case 1:
                        _discountprice = _discountprice - (decimal)_discountvalue;
                        break;
                    case 2:
                        _discountprice = (decimal)_discountvalue;
                        break;
                }
                return _discountprice;
            }
        }
        private decimal _discountprice;
        private string _pshowimg;//商品图片

        /// <summary>
        /// 商品图片
        /// </summary>
        [DataMember]
        public string Pshowimg
        {
            get { return _pshowimg; }
            set { _pshowimg = value; }
        }

    }

}