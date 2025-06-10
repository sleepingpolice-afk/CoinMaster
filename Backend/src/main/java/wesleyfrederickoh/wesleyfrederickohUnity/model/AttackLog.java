package wesleyfrederickoh.wesleyfrederickohUnity.model;

import jakarta.persistence.*;
import org.hibernate.annotations.UuidGenerator;

import java.time.LocalDateTime;
import java.util.UUID;

@Entity
@Table(name = "attack_log")
public class AttackLog {

    @Id
    @GeneratedValue
    @UuidGenerator
    @Column(name = "LogID", updatable = false, nullable = false)
    private UUID logId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "AttackerID", nullable = false)
    private Player attacker;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "DefenderID", nullable = false)
    private Player defender;

    @Column(name = "attacked", nullable = false)
    private LocalDateTime attackedAt;

    public AttackLog() {
        this.attackedAt = LocalDateTime.now();
    }

    public AttackLog(Player attacker, Player defender) {
        this.attacker = attacker;
        this.defender = defender;
        this.attackedAt = LocalDateTime.now();
    }

    // --- Getters and Setters ---

    public UUID getLogId() {
        return logId;
    }

    public void setLogId(UUID logId) {
        this.logId = logId;
    }

    public Player getAttacker() {
        return attacker;
    }

    public void setAttacker(Player attacker) {
        this.attacker = attacker;
    }

    public Player getDefender() {
        return defender;
    }

    public void setDefender(Player defender) {
        this.defender = defender;
    }

    public LocalDateTime getAttackedAt() {
        return attackedAt;
    }

    public void setAttackedAt(LocalDateTime attackedAt) {
        this.attackedAt = attackedAt;
    }
}
