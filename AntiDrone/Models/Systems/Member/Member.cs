using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Systems.Member;
/* 회원 데이터 모델 */
public class Member
{
    [Key]
    public long id { get; set; } /* index */
    public long authority { get; set; } /* 회원 권한 */
    public long permission_state { get; set; } /* 가입 승인 상태 */
    
    [Required(ErrorMessage ="사용할 아이디를 입력하세요.")]
    public string member_id { get; set; } /* 회원 아이디 */
    [Required(ErrorMessage ="사용할 비밀번호를 입력하세요.")]
    public string member_pw { get; set; } /* 회원 패스워드 */
    [Required(ErrorMessage ="사용자 이름을 입력하세요.")]
    public string member_name { get; set; } /* 회원명 */
    [Required(ErrorMessage ="이메일 주소를 입력하세요.")]
    public string member_email { get; set; } /* 회원 이메일 */
    [Required(ErrorMessage ="연락처를 입력하세요.")]
    public string member_contact { get; set; } /* 회원 연락처 */
    
    public DateTime join_datetime { get; set; } /* 가입 일시 */
    public DateTime latest_access_datetime { get; set; } /* 최근 접속 일시 */
}