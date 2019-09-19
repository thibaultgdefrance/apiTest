//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIMasterCode.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Messagerie
    {
        public int IdMessagerie { get; set; }
        public string Texte { get; set; }
        public System.DateTime DatePublication { get; set; }
        public System.DateTime DateFin { get; set; }
        public Nullable<int> IdMessagerieParent { get; set; }
        public Nullable<int> IdAuteur { get; set; }
        public int IdStatut { get; set; }
        public Nullable<int> IdAcces { get; set; }
    
        public virtual Acces Acces { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
        public virtual Statut Statut { get; set; }
    }
}