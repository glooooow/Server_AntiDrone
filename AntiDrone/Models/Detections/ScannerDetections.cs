using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Detections;
/* 스캐너 탐지 데이터 모델 */
public class ScannerDetections
{
    [Key]
    public long id { get; set; } /* index */
    public string type { get; set; } /* 탐지 장비(유형)명 */
    public string code { get; set; } /* 탐지 데이터 코드 */
    public string port { get; set; } /* 탐지 데이터 유입 port */
    public string sc_det_id { get; set; } /* 스캐너 탐지체 고유 식별 id */
    public string model { get; set; } /* 탐지 드론 모델명 */
    public double latitude { get; set; } /* 탐지 위도값 */
    public double longitude { get; set; } /* 탐지 경도값 */
    public double altitude { get; set; } /* 탐지 고도값 */
    public double elevation { get; set; } /* GCS 탐지 고도값(m) */
    public double velocity { get; set; } /* 탐지 속도값 */
    public double course { get; set; } /* 탐지 코스각 */
    public double angle { get; set; } /* 방향탐지 각도(-180˚~180˚) */
    public double range { get; set; } /* 정확성 각도(0˚~360˚) */
    public double frequency { get; set; } /* 방향탐지 주파수(㎐) */
    public double operator_lat { get; set; } /* 조종자 좌표 위도값 */
    public double operator_lon { get; set; } /* 조종자 좌표 경도값 */
    public string mfr { get; set; } /* 탐지 드론의 제조사 */
    public string protocol { get; set; } /* 식별 프로토콜 */
    public string mac_address1 { get; set; } /* MAC 주소 1 */
    public string mac_address2 { get; set; } /* MAC 주소 2 */
    
    public DateOnly det_date { get; set; } /* 비행 탐지 일자 */
    public TimeOnly det_time { get; set; } /* 비행 탐지 시각 */
}