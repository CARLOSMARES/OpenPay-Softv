﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SoftvWCFService.Entities
{
    /// <summary>
    /// Class                   : Softv.Entities.BanamexEntity.cs
    /// Generated by            : Class Generator (c) 2014
    /// Description             : Banamex entity
    /// File                    : BanamexEntity.cs
    /// Creation date           : 19/07/2017
    /// Creation time           : 11:31 a. m.
    ///</summary>
    [DataContract]
    [Serializable]
    public class BanamexEntity 
    {
        #region Attributes

        /// <summary>
        /// Property idInt
        /// </summary>
        [DataMember]
        public long? idInt { get; set; }

        /// <summary>
        /// Property merchant
        /// </summary>
        [DataMember]
        public String merchant { get; set; }
        /// <summary>
        /// Property result
        /// </summary>
        [DataMember]
        public String result { get; set; }
        /// <summary>
        /// Property successIndicator
        /// </summary>
        [DataMember]
        public String successIndicator { get; set; }

        /// <summary>
        /// Property id
        /// </summary>
        [DataMember]
        public String id { get; set; }
        /// <summary>
        /// Property amount
        /// </summary>
        [DataMember]
        public String amount { get; set; }
        /// <summary>
        /// Property currency
        /// </summary>
        [DataMember]
        public String currency { get; set; }

        
        
        #endregion
    }

    [DataContract]
    [Serializable]
    public class DatosMovimientoEntity
    {
        #region Attributes

        /// <summary>
        /// Property idInt
        /// </summary>
        [DataMember]
        public string sessionId { get; set; }

        [DataMember]
        public string userID { get; set; }

        [DataMember]
        public string descripcionImporte { get; set; }

        [DataMember]
        public string merchantName { get; set; }

        [DataMember]
        public string merchantId { get; set; }

        [DataMember]
        public string addressLine1 { get; set; }

        [DataMember]
        public string addressLine2 { get; set; }

        [DataMember]
        public string email { get; set; }

        #endregion
    }
}
