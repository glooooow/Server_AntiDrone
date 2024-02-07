using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Systems.DroneControl;
/* 비행 승인 드론 데이터 모델 */
public class Whitelist
{
    [Key]
    public long id { get; set; } /* index */
    public string affiliation { get; set; } /* 소속 단체명 */
    public string operator_name { get; set; } /* 조종자 이름 */
    public string contact { get; set; } /* 연락처 */
    public string drone_type { get; set; } /* 드론 분류(DJI 또는 WiFi) */
    public string drone_model { get; set; } /* 드론 모델명 */
    public string drone_id { get; set; } /* 드론 식별자 */
    public string memo { get; set; } /* 특이사항 메모 */
    public string approval_state { get; set; } /* 승인 상태 : 0-미승인, 1-승인, 2-위협 (int값으로 추후 변경) */
    public DateOnly approval_start_date { get; set; } /* 비행 승인 시작 일자 */
    public DateOnly approval_end_date { get; set; } /* 비행 승인 종료 일자 */
    public DateOnly now_date { get; set; } /* 현재 일자 */
}