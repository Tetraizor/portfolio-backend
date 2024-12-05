-- init-db.sql
CREATE DATABASE IF NOT EXISTS portfolio;

USE portfolio;

CREATE TABLE IF NOT EXISTS posts (
    post_id VARCHAR(36) PRIMARY KEY NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    title VARCHAR(255) NOT NULL,
    content MEDIUMTEXT NOT NULL,
    url_string CHAR(255) NOT NULL,
    views INT DEFAULT 0,
    thumbnail_url VARCHAR(255) NOT NULL,
    is_pinned BOOLEAN DEFAULT FALSE,
    is_hidden BOOLEAN DEFAULT FALSE,
    is_draft BOOLEAN DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS featured_items (
    featured_id VARCHAR(36) PRIMARY KEY NOT NULL,
    priority INT DEFAULT 10,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    thumbnail_url VARCHAR(255) NOT NULL,
    title VARCHAR(255) NOT NULL,
    content TEXT NOT NULL,
    target_url VARCHAR(255)
);