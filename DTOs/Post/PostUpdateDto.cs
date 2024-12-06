using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PortfolioService.DTOs.Post
{
    public record class PostUpdateDto
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
        public int Views { get; set; }

        [JsonPropertyName("thumbnail_url")]
        [Required(ErrorMessage = "Thumbnail URL is required.")]
        [StringLength(255, ErrorMessage = "Thumbnail URL cannot be longer than 255 characters.")]
        public required string ThumbnailUrl { get; set; }

        [JsonPropertyName("is_pinned")]
        public required bool IsPinned { get; set; } = false;

        [JsonPropertyName("is_hidden")]
        public required bool IsHidden { get; set; } = false;

        [JsonPropertyName("is_draft")]
        public required bool IsDraft { get; set; } = false;
    }
}