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
    
    public partial class Trophee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trophee()
        {
            this.Utilisateur = new HashSet<Utilisateur>();
        }
    
        public int IdTrophee { get; set; }
        public string typeTrophee { get; set; }
        public string cheminIcone { get; set; }
        public int Seuil { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Utilisateur> Utilisateur { get; set; }
    }
}