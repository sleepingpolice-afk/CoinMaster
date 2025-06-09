package wesleyfrederickoh.wesleyfrederickohUnity.dto;

import java.time.LocalDateTime;
import java.util.Map;
import java.util.UUID;

public class PlayerDataResponse {
    private UUID playerId;
    private long currency;
    private LocalDateTime createdAt;
    private String username;
    private String email;
    private Map<String, Integer> upgradeLevels;

    public PlayerDataResponse() {}

    public PlayerDataResponse(UUID playerId, long currency, LocalDateTime createdAt, String username, String email, Map<String, Integer> upgradeLevels) {
        this.playerId = playerId;
        this.currency = currency;
        this.createdAt = createdAt;
        this.username = username;
        this.email = email;
        this.upgradeLevels = upgradeLevels;
    }

    public UUID getPlayerId() {
        return playerId;
    }

    public void setPlayerId(UUID playerId) {
        this.playerId = playerId;
    }

    public long getCurrency() {
        return currency;
    }

    public void setCurrency(long currency) {
        this.currency = currency;
    }

    public LocalDateTime getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(LocalDateTime createdAt) {
        this.createdAt = createdAt;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public Map<String, Integer> getUpgradeLevels() {
        return upgradeLevels;
    }

    public void setUpgradeLevels(Map<String, Integer> upgradeLevels) {
        this.upgradeLevels = upgradeLevels;
    }
}
