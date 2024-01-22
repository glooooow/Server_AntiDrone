﻿using System.ComponentModel.DataAnnotations;

namespace AntiDrone.Models.Systems.Member;
/* 회원 변경 및 접속 로그 데이터 모델 */
public class MemberLog
{
    [Key]
    public long id { get; set; } /* index */
    public string memlog_message { get; set; } /* 로그 메세지 */
    public string memlog_level { get; set; } /* 로그 단계 */
    public string memlog_from { get; set; } /* 로그 발생 주체 회원(아이디) */
    public string memlog_to { get; set; } /* 로그 내용 해당 회원(아이디_ */
    public DateTime memlog_datetime { get; set; } /* 로그 발생일시 */
}