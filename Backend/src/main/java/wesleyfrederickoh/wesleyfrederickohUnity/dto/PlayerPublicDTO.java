package wesleyfrederickoh.wesleyfrederickohUnity.dto;

import java.util.UUID;

public class PlayerPublicDTO {

    private UUID playerId;
    private String username;
    private Long currency;

    public PlayerPublicDTO() {
    }

    public PlayerPublicDTO(UUID playerId, String username, Long currency) {
        this.playerId = playerId;
        this.username = username;
        this.currency = currency;
    }

    public UUID getPlayerId() {
        return playerId;
    }

    public void setPlayerId(UUID playerId) {
        this.playerId = playerId;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public Long getCurrency() {
        return currency;
    }

    public void setCurrency(Long currency) {
        this.currency = currency;
    }
}