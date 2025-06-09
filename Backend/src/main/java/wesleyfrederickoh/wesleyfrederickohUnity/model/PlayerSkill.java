package wesleyfrederickoh.wesleyfrederickohUnity.model;

import jakarta.persistence.*;
import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

import java.util.UUID;

@Entity
@Table(name = "player_skills")
public class PlayerSkill {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name = "PSkillsID", updatable = false, nullable = false)
    @JdbcTypeCode(SqlTypes.UUID)
    private UUID id;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "PlayerID", nullable = false)
    private Player player;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "SkillID", nullable = false)
    private Skill skill;

    public PlayerSkill() {}

    public PlayerSkill(Player player, Skill skill) {
        this.player = player;
        this.skill = skill;
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

    public Skill getSkill() {
        return skill;
    }

    public void setSkill(Skill skill) {
        this.skill = skill;
    }
}
