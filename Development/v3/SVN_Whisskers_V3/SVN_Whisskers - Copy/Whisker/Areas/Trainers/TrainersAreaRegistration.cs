using System.Web.Mvc;

namespace Whisker.Areas.Trainers
{
    public class TrainersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Trainers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Trainers_default",
                "Trainers/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
