package wesleyfrederickoh.wesleyfrederickohUnity.model;

import jakarta.persistence.*;
import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

import java.util.UUID;

@Entity
@Table(name = "player_upgrades") // Match dump.sql table name
public class PlayerUpgrade {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO) // Let Hibernate handle UUID generation with DB default
    @Column(name = "PUpgradesID", updatable = false, nullable = false)
    @JdbcTypeCode(SqlTypes.UUID)
    private UUID id;

    @ManyToOne
    @JoinColumn(name = "PlayerID", nullable = false) // Matched column name
    private Player player;

    @ManyToOne
    @JoinColumn(name = "UpgradeID", nullable = false) // Matched column name
    private UpgradeType upgradeType;

    @Column(name = "Level", nullable = false) // Matched column name
    private int level;

    public PlayerUpgrade() {}

    public PlayerUpgrade(Player player, UpgradeType upgradeType, int level) {
        this.player = player;
        this.upgradeType = upgradeType;
        this.level = level;
    }

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public Player getPlayer() {
        return player;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public UpgradeType getUpgradeType() {
        return upgradeType;
    }

    public void setUpgradeType(UpgradeType upgradeType) {
        this.upgradeType = upgradeType;
    }

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }
}
