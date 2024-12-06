using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PortfolioService.DTOs.Post;

public record class PostCreateDto
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("title")]
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(255, ErrorMessage = "Title cannot be longer than 255 characters.")]
    public required string Title { get; set; }

    [JsonPropertyName("content")]
    [Required(ErrorMessage = "Content is required.")]
    public required string Content { get; set; }

    [JsonPropertyName("url_string")]
    [Required(ErrorMessage = "URL string is required.")]
    [StringLength(255, ErrorMessage = "URL string cannot be longer than 255 characters.")]
    public required string UrlString { get; set; }

    [JsonPropertyName("views")]
    [Range(0, int.MaxValue, ErrorMessage = "Views must be a positive integer.")]
    public int Views { get; set; } = 0;

    [JsonPropertyName("thumbnail_url")]
    [StringLength(255, ErrorMessage = "Thumbnail URL cannot be longer than 255 characters.")]
    public string ThumbnailUrl { get; set; } = "";

    [JsonPropertyName("is_pinned")]
    public bool IsPinned { get; set; } = false;

    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; } = false;

    [JsonPropertyName("is_draft")]
    public bool IsDraft { get; set; } = false;
}