using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AntiDrone.Models.Systems.Member;
/* 회원 데이터 모델 */
public class Member
{
    [Key]
    public long id { get; set; } /* index */

    public int authority { get; set; } = 3; /* 회원 권한 : 0=미할당, 1=관리, 2=운영, 3=일반 */
    public int permission_state { get; set; } = -1; /* 가입 승인 상태 : -1=승인대기, 1=승인완료 */
    
    [Required(ErrorMessage ="아이디를 입력하세요.")]
    public string member_id { get; set; } /* 회원 아이디 */
    [Required(ErrorMessage ="비밀번호를 입력하세요.")]
    public string member_pw { get; set; } /* 회원 패스워드 */
    [Required(ErrorMessage ="사용자 이름을 입력하세요.")]
    public string member_name { get; set; } /* 회원명 */
    
    public DateTime join_datetime { get; set; } /* 가입 일시 */
    public DateTime latest_access_datetime { get; set; } /* 최근 접속 일시 */

    public class MemberBasicInfo
    {
        public int authority { get; set; }
        public string member_name { get; set; }
    }
}

public class LoginModel
{
    [Required(ErrorMessage ="올바른 아이디를 입력하세요.")]
    public string member_id { get; set; }
    [Required(ErrorMessage ="올바른 비밀번호를 입력하세요.")]
    public string member_pw { get; set; }
}

public class UpdateMemberInfo
{
    public int authority { get; set; }
    public int permission_state { get; set; }
    public string? member_pw { get; set; }
}