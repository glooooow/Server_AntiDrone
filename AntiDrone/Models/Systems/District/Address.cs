namespace AntiDrone.Models.Systems.District;
/* 주소 데이터 모델, NON-PK */
public class Address
{
    public string law_code { get; set; } /* 법정동 코드 */
    public string district_code { get; set; } /* 행정동 코드 */
    public string bunji { get; set; } /* 번지 */
    public string add_xcoord { get; set; } /* 경도 */
    public string add_ycoord { get; set; } /* 위도 */
}