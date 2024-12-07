using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PortfolioService.DTOs.Featured;

public record class FeaturedItemDto
{
    [JsonPropertyName("featured_id")]
    [Required(ErrorMessage = "Featured ID is required.")]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Featured ID must be 36 characters.")]
    public required string FeaturedId { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("title")]
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(255, ErrorMessage = "Title cannot be longer than 255 characters.")]
    public required string Title { get; set; }

    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    [JsonPropertyName("thumbnail_url")]
    [Required(ErrorMessage = "Thumbnail URL is required.")]
    [StringLength(255, ErrorMessage = "Thumbnail URL cannot be longer than 255 characters.")]
    public required string ThumbnailUrl { get; set; }

    [JsonPropertyName("target_url")]
    [Required(ErrorMessage = "Target URL is required.")]
    [StringLength(255, ErrorMessage = "Target URL cannot be longer than 255 characters.")]
    public required string TargetUrl { get; set; }

    [JsonPropertyName("content")]
    [Required(ErrorMessage = "Content is required.")]
    public required string Content { get; set; }
}