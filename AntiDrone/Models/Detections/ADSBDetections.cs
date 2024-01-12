using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Detections;
/* ADS-B 탐지 데이터 모델 */
public class ADSBDetections
{
    [Key]
    public long id { get; set; } /* index */
    public string type { get; set; } /* 탐지 장비(유형)명 */
    public string code { get; set; } /* 탐지 데이터 코드 */
    public string data_name { get; set; } /* 탐지 데이터명(편명) */
    public double latitude { get; set; } /* 탐지 위도값 */
    public double longitude { get; set; } /* 탐지 경도값 */
    public double altitude { get; set; } /* 탐지 고도값 */
    public double velocity { get; set; } /* 탐지 속도값 */
    public double course { get; set; } /* 탐지 코스각 */
    public DateOnly det_date { get; set; } /* 탐지 일자 */
    public TimeOnly det_time { get; set; } /* 탐지 시각 */
}