using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Shields;

public class JammerRadiations
{
    [Key]
    public long id { get; set; } /* index */
    public string code { get; set; } /* 탐지 데이터 코드 */
    public string agent { get; set; } /* 방사 작업자 */
    public string body_num { get; set; } /* 재머 본체 식별 번호 : ex) 1번 재머, 2번 재머 */
    public bool jam_result { get; set; } /* 방사 결과 : true-O, false-X */
    public bool jam_type { get; set; } /* 방사 종류 : true-자동, false-수동 */
    public string mfr { get; set; } /* 재머 제조사 */
    public double pan { get; set; } /* 재머 방위각(좌우) */
    public double pan_last { get; set; } /* ???? */
    public double tilt { get; set; } /* 재머 고각(상하) */
    public double tilt_last { get; set; } /* ???? */
    public double print_400 { get; set; } /* RF 400MHz 출력값 */
    public double print_900 { get; set; } /* RF 900MHz 출력값 */
    public double print_l1 { get; set; } /* RF GNSS L1 출력값 */
    public double print_l2 { get; set; } /* RF GNSS L2 출력값 */
    public double print_2400 { get; set; } /* RF 2.4GHz 출력값 */
    public double print_5800 { get; set; } /* RF 5.8GHz 출력값 */
    
    public DateTime start_datetime { get; set; } /* 방사 시작 일시 */
    public DateTime end_datetime { get; set; } /* 방사 종료 일시 */
}