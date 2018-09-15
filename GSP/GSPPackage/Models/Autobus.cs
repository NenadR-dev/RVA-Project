///////////////////////////////////////////////////////////
//  Autobus.cs
//  Implementation of the Class Autobus
//  Generated by Enterprise Architect
//  Created on:      23-Jul-2018 11:19:17 AM
//  Original author: Mihailo
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GSPPackage.Models
{
    [Table("BusTable")]
    [DataContract]
	public class Autobus {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int ID { get; set; }
        
       // public int LinijaID { get; set; }
        [DataMember]
        public string Oznaka { get; set; }

        public Autobus(){

		}

		~Autobus(){

		}

    }//end Autobus

}//end namespace GSP