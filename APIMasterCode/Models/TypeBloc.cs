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
    
    public partial class TypeBloc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeBloc()
        {
            this.Bloc = new HashSet<Bloc>();
            this.Media = new HashSet<Media>();
        }
    
        public int IdTypeBloc { get; set; }
        public string NomBloc { get; set; }
        public int IdAcces { get; set; }
        public int IdStatut { get; set; }
    
        public virtual Acces Acces { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bloc> Bloc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Media> Media { get; set; }
        public virtual Statut Statut { get; set; }
    }
}
