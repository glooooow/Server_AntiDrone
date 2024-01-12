using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Systems;
/* 시스템 로그 데이터 모델 */
public class SystemLog
{
    [Key]
    public long id { get; set; } /* index */
    public string syslog_message { get; set; } /* 시스템 로그 메세지 */
    public string syslog_level { get; set; } /* 시스템 로그 단계 */
    public string syslog_from { get; set; } /* 시스템 로그 발생 주체 */
    public DateTime syslog_datetime { get; set; } /* 시스템 로그 발생일시 */
}