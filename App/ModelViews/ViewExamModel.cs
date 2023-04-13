using Examination_system.Models;

namespace Examination_system.ModelViews
{
    public class ViewExamModel
    {
        public string QustBody { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
