//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.Collections.Generic;

    public partial class Student : IEntity
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }

        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public Nullable<int> StandardId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Standard Standard { get; set; }
        public virtual StudentAddress StudentAddress { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public EntityState EntityState { get; set; }
    }
}
