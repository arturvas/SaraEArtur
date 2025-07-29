namespace Wedding.API.Core.DTOs;

public record CustomGiftDto(
    decimal Amount,
    string PayerName,
    string PayerSurname
    );
