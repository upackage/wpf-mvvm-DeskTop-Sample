using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZFS.Model.Entity
{
    public interface IBaseEntity
    {
        int Id { get; set; }
    }

    [Serializable]
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}