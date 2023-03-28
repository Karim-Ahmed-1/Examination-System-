using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Examination_system.ModelViews
{
    public class LoginMV
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Type { get; set; }

    }
}
