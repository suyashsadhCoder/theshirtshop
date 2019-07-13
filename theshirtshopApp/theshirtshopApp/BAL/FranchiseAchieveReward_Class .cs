using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bal_TheShirtShop
{
    public class FranchiseAchieveReward_Class 
    {
        public int FranchiseAchieve_RewardId { get; set; }
        public int Franchise_Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public decimal Total_Amount { get; set; }
       
        public int Created_By { get; set; }

        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }

        public bool Status { get; set; }

    }

    public class FranchiseAchieveRewardDetail_Class
    {

        public int FranchiseAchieve_RewardDetailId { get; set; }
        public int FranchiseAchieve_RewardId { get; set; }
        public int Reward_Id { get; set; }
        public int Purchase_Point { get; set; }

        public decimal Amount_OnePoint { get; set; }
        public decimal Totla_Amount { get; set; }

        public int Start_Point { get; set; }
        public int End_Point { get; set; }



    }
}
