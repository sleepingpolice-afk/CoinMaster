package wesleyfrederickoh.wesleyfrederickohUnity.model;

import jakarta.persistence.*;
import java.time.OffsetDateTime;
import java.util.UUID;
import java.util.Set;
import java.sql.Timestamp;


@Entity
@Table(name = "Player")
public class Player {
    @Id
    @GeneratedValue
    @Column(name = "PlayerID", updatable = false, nullable = false)
    private UUID playerId;

    private long currency;

    @Column(name = "created_at", nullable = false, updatable = false, insertable = false)
    private Timestamp createdAt;

    @Column(nullable = false, unique = true)
    private String username;

    @Column(nullable = false)
    private String password;

    @Column(nullable = false, unique = true)
    private String email;

    // Getters and setters

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

    public Timestamp getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Timestamp createdAt) {
        this.createdAt = createdAt;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }
}