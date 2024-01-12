using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Detections;
/* 레이터 탐지 예측 데이터 모델 */
public class RadarPredictions
{
    [Key]
    public long id { get; set; } /* index */
    public string type { get; set; } /* 탐지 장비(유형)명 */
    public string code { get; set; } /* 연관된 탐지 데이터 코드 */
    public long ra_det_id { get; set; } /* 레이더 탐지체 고유 식별 id */
    public long points { get; set; } /* 탐지체 이동 감지 예측 횟수 */
    public double latitude { get; set; } /* 탐지 예측 위도값 */
    public double longitude { get; set; } /* 탐지 예측 경도값 */
    public double altitude { get; set; } /* 탐지 예측 고도값 */
    public double alititude_sea { get; set; } /* 탐지 예측 해발 고도값 */
    public double alititude_surface { get; set; } /* 탐지 예측 지표면 고도값 */
    public double elevation { get; set; } /* 장비로부터의 탐지 예측 고도값 */
    public double velocity { get; set; } /* 탐지 예측 속도값 */
    public double radial_speed { get; set; } /* 탐지체로 방사한 예측 레이더 속도 */
    public double tanential_speed { get; set; } /* 탐지체에 접한 예측 레이더 속도 */
    public double azimuth_gps { get; set; } /* 탐지체 GPS 예측 방위각 */
    public double azimuth_radar { get; set; } /* 탐지체 레이다 예측 방위각 */
    public double radius { get; set; } /* 연관된 탐지체의 반경 */
    public double range { get; set; } /* ????????? */
    public double rcs { get; set; } /* ????????? */
    public double class_name { get; set; } /* 연관된 탐지체 식별 유형(드론/새/차/사람) */
    
    public DateOnly det_date { get; set; } /* 연관된 탐지 일자 */
    public TimeOnly det_time { get; set; } /* 탐지 예측 시각 */
}