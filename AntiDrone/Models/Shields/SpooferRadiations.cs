using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Shields;

public class SpooferRadiations
{
    [Key]
    public long id { get; set; } /* index */
    public string agent { get; set; } /* 방사 작업자 */
    public short body_num { get; set; } /* 스푸퍼 본체 번호 : ex) 1번 스푸퍼, 2번 스푸퍼 */
    public string pattern { get; set; } /* 방사 형태 */
    public double print { get; set; } /* 방사 출력값 */
    public double rf_offset { get; set; } /* 주파수 출력 조정값 */
    public double radius { get; set; } /* 기만 반경(m) */
    public double velocity { get; set; } /* 각 속도(m/s) */
    
    // 글로벌위성항법시스템(GNSS) 운용 모델 : GPS-미국, BeiDou-중국, GLONASS-러시아, Galileo-유럽
    public short L1CA { get; set; } /* GPS-L1CA  */
    public short L2C { get; set; } /* GPS-L2C */
    public short B1 { get; set; } /* BeiDou-B1 */
    public short B2 { get; set; } /* BeiDou-B2 */
    public short G1 { get; set; } /* GLONASS-G1 */
    public short G2 { get; set; } /* GLONASS-G2 */
    public short E1 { get; set; } /* Galileo-E1 */
    public short E5b { get; set; } /* Galileo-E5b */
    
    public DateTime start_datetime { get; set; } /* 방사 시작 일시 */
    public DateTime end_datetime { get; set; } /* 방사 종료 일시 */
}