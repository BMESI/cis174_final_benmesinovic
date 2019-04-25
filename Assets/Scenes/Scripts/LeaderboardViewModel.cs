using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardViewModel
{
    
    public Guid HighScoreId { get; set; }
    public Guid PersonId { get; set; }
    public int Score { get; set; }
    public DateTime? DateAttained { get; set; }
  
    /*
    public Guid PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime? DateCreated { get; set; }
    */
}
