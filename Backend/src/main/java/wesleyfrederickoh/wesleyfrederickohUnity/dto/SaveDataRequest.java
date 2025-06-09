package wesleyfrederickoh.wesleyfrederickohUnity.dto;

import java.util.Map;
import java.util.UUID; // Import UUID

public class SaveDataRequest {
    private String playerId; // Changed from username, email, password to playerId
    private long currency;
    private Map<String, Integer> upgradeLevels;

    public String getPlayerId() {
        return playerId;
    }

    public void setPlayerId(String playerId) {
        this.playerId = playerId;
    }

    public long getCurrency() {
        return currency;
    }

    public void setCurrency(long currency) {
        this.currency = currency;
    }

    public Map<String, Integer> getUpgradeLevels() {
        return upgradeLevels;
    }

    public void setUpgradeLevels(Map<String, Integer> upgradeLevels) {
        this.upgradeLevels = upgradeLevels;
    }
}
