package wesleyfrederickoh.wesleyfrederickohUnity.model;

import jakarta.persistence.*;
import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

import java.util.HashSet;
import java.util.Set;
import java.util.UUID;

@Entity
@Table(name = "Skill")
public class Skill {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name = "SkillID", updatable = false, nullable = false)
    @JdbcTypeCode(SqlTypes.UUID)
    private UUID id;

    @Column(name = "Name", unique = true, nullable = false)
    private String name;

    @Column(name = "duration", nullable = false)
    private float duration;

    @Column(name = "cooldown", nullable = false)
    private float cooldown;

    @Column(name = "description", nullable = false, columnDefinition = "TEXT")
    private String description;

    @OneToMany(mappedBy = "skill", cascade = CascadeType.ALL, orphanRemoval = true)
    private Set<PlayerSkill> playerSkills = new HashSet<>();

    public Skill() {}

    public Skill(String name, float duration, float cooldown, String description) {
        this.name = name;
        this.duration = duration;
        this.cooldown = cooldown;
        this.description = description;
    }

    // Getters and Setters
    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public float getDuration() {
        return duration;
    }

    public void setDuration(float duration) {
        this.duration = duration;
    }

    public float getCooldown() {
        return cooldown;
    }

    public void setCooldown(float cooldown) {
        this.cooldown = cooldown;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Set<PlayerSkill> getPlayerSkills() {
        return playerSkills;
    }

    public void setPlayerSkills(Set<PlayerSkill> playerSkills) {
        this.playerSkills = playerSkills;
    }
}
