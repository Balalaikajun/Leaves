using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Data.Models;


public partial class Account
{
    public int PkAccountid { get; set; }

    public int FkUserid { get; set; }

    public decimal Balance { get; set; }

    public DateTime Created { get; set; }

    [JsonIgnore]
    public virtual User FkUser { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<Transaction> TransactionFkAccountidfromNavigations { get; set; } = new List<Transaction>();
    
    [JsonIgnore]
    public virtual ICollection<Transaction> TransactionFkAccountidtoNavigations { get; set; } = new List<Transaction>();
}
