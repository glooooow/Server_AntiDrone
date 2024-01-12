using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Detections;
/* 탐지 이력 헤더 데이터 모델 */
public class HistoryHeader
{
    [Key]
    public long meta_master_key { get; set; } /* Master-key값 */
    public string meta_data_code { get; set; } /* 데이터 코드 */ /* FK? */  
    public string meta_data_name { get; set; } /* 데이터명 */
    public string meta_data_type { get; set; } /* 데이터 유형 */
    public string meta_data_id { get; set; } /* 데이터 ID(스캐너ID, 레이더ID) */
    public string meta_writer { get; set; } /* 메모 작성자 */
    public string meta_memo { get; set; } /* 메모 내용 */
    public double meta_near_lat { get; set; } /* 가까운 좌표(위도) */
    public double meta_near_lon { get; set; } /* 가까운 좌표(경도) */
    public double meta_near_operator_lat { get; set; } /* 가까운 조종자 좌표(위도) */
    public double meta_near_operator_lon { get; set; } /* 가까운 조종자 좌표(경도) */
    public string meta_mfr { get; set; } /* 드론 모델 제조사명 */
    
    public DateOnly det_date { get; set; } /* 탐지 일자 */
    public TimeOnly det_start_time { get; set; } /* 탐지 시작 시각 */
    public TimeOnly det_end_time { get; set; } /* 탐지 종료 시각 */
}