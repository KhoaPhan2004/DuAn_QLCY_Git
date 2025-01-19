using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime NotificationTime { get; set; }

    public bool? IsRead { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
