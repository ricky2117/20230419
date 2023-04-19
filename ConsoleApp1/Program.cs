using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var obj = new LeaveHours();
			if (!obj.IsWorkingDay()) return; 
			obj.LeaveTimeCalcilation();
			Console.ReadLine();
		}
	}
	public class LeaveHours
	{

		private DateTime _Begin_time;
		private DateTime _End_time;

		public LeaveHours() { }

		//檢查輸入時間 是否符合上班時段
		public  bool IsWorkingDay()
		{   
			//BEGIN
			bool flag = false;
			DateTime Begin_time;
			int Begin_week;
			while (!flag)
			{
				flag = true;
				Console.WriteLine("請輸入請假起日(yyy,MM,nn HH:mm:)");
				flag = DateTime.TryParse(Console.ReadLine(), out Begin_time);  //格式
				Begin_week = int.Parse(Begin_time.DayOfWeek.ToString("d"));  //周1~5

				if (!flag) { Console.WriteLine("您輸入起日錯誤，請再輸入一次"); }
				else if (!(Begin_week >= 1 && Begin_week <= 5)) { Console.WriteLine("您輸入起日非工作周，請再輸入一次"); flag = false; }

			    _Begin_time = Begin_time;		
			}

			//END
			flag = false;
			DateTime End_time;
			int End_week;
			while (!flag)
			{
				flag = true;
				Console.WriteLine("請輸入請假迄日(yyy,MM,nn HH:mm:)");
				flag = DateTime.TryParse(Console.ReadLine(), out End_time);
                End_week = int.Parse(End_time.DayOfWeek.ToString("d"));

				if (!flag) { Console.WriteLine("您輸入迄日錯誤，請再輸入一次"); }
				else if (!(End_week >= 1 && End_week <= 5)) { Console.WriteLine("您輸入迄日非工作周，請再輸入一次"); flag = false; }

			    _End_time = End_time;
            }
			if (_Begin_time > _End_time) { Console.WriteLine("你輸入反了，88"); Console.ReadLine(); return false; }

			return flag;
		}

		public void LeaveTimeCalcilation()
		{
			DateTime Begin_time = _Begin_time;
			DateTime End_time = _End_time;

			if (Begin_time.Hour < 9) Begin_time = new DateTime(Begin_time.Year,Begin_time.Month,Begin_time.Day,9,0,0);
			if (End_time.Hour > 18) End_time = new DateTime(End_time.Year, End_time.Month, End_time.Day,18,0,0);
			if (End_time.Hour < 9) End_time = new DateTime(End_time.Year, End_time.Month, End_time.Day, 9, 0, 0);

			TimeSpan ts = End_time - Begin_time;
			double hours = ts.TotalHours;

			if (Begin_time.Hour < 12 && End_time.Hour > 13) { hours--; Console.WriteLine("你請假的時數是:" + hours + "含中間休息一小時"); }
			else { Console.WriteLine("你請假的時數是:" + hours); }			
        }
	}
}
