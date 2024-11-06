using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Data.Models;

public partial class User
{
    public int PkUserid { get; set; }

    public string UqLogin { get; set; } = null!;
    [JsonIgnore]
    public string Userpassword { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
