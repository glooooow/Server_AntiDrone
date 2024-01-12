using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Systems.DroneControl;

public class PDF
{
    [Key]
    public long id { get; set; } /* index */
    public string meta_data_code { get; set; } /* 탐지 헤더 데이터 코드 */
    public string acquisitor { get; set; } /* 기재 정보 취득처 */
    public string writer { get; set; } /* 작성자명 */
    public string watch_time { get; set; } /* 육안감시_시간 */
    public string watch_place { get; set; } /* 육안감시_장소 */
    public string watch_direction { get; set; } /* 육안감시_방향 */
    public string watch_memo { get; set; } /* 육안감시_특이사항 */
    
    
    //작성자 특이사항란(시간대 작성란)
    public string significant_time1 { get; set; } /* 시간대 작성란 1 */
    public string significant_time2 { get; set; } /* 시간대 작성란 2 */
    public string significant_time3 { get; set; } /* 시간대 작성란 3 */
    public string significant_time4 { get; set; } /* 시간대 작성란 4 */
    public string significant_time5 { get; set; } /* 시간대 작성란 5 */
    public string significant_time6 { get; set; } /* 시간대 작성란 6 */
    public string significant_time7 { get; set; } /* 시간대 작성란 7 */
    //작성자 특이사항란(시간대별 내용 작성란)
    public string significant_content1 { get; set; } /* 내용 작성란 1 */
    public string significant_content2 { get; set; } /* 내용 작성란 2 */
    public string significant_content3 { get; set; } /* 내용 작성란 3 */
    public string significant_content4 { get; set; } /* 내용 작성란 4 */
    public string significant_content5 { get; set; } /* 내용 작성란 5 */
    public string significant_content6 { get; set; } /* 내용 작성란 6 */
    public string significant_content7 { get; set; } /* 내용 작성란 7 */
    //작성자 특이사항란(기타, 각 주석 참고)
    public string close_situation { get; set; } /* 상황 종료 내용 */
    public string flight_reason { get; set; } /* 비행 사유 */
    public string flight_time { get; set; } /* 비행 시간 */
    
    
    //보고자 기재란(부처,부서별 구분)
    public string internal_reporter1 { get; set; } /* 내부 보고자1 */
    public string internal_reporter2 { get; set; } /* 내부 보고자2 */
    public string internal_reporter3 { get; set; } /* 내부 보고자3 */
    public string regulatory_reporter1 { get; set; } /* 규제기관 보고자1 */
    public string regulatory_reporter2 { get; set; } /* 규제기관 보고자2 */
    public string police_reporter1 { get; set; } /* 경찰 보고자1 */
    public string police_reporter2 { get; set; } /* 경찰 보고자2 */
    public string military_reporter1 { get; set; } /* 군 보고자2 */
    public string military_reporter2 { get; set; } /* 군 보고자2 */
    public string organization { get; set; } /* 기관 보고 확인자 */
    
    public DateTime create_datetime { get; set; } /* 최초 작성 일시 */
}