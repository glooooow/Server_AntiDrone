namespace AntiDrone.Models.Systems.District;
/* 지역지구 데이터 모델, NON-PK */
public class Area
{
    public string law_code { get; set; } /* 법정동 코드 */
    public string area_do { get; set; } /* 광역시·도 */
    public string area_si { get; set; } /* 시·군·구 */
    public string area_dong { get; set; } /* 읍·면·동 */
    public string area_ri { get; set; } /* 리 */
    public string area_size { get; set; } /* 지역 면적 */
    public string area_sort { get; set; } /* 지역 구분 */
}