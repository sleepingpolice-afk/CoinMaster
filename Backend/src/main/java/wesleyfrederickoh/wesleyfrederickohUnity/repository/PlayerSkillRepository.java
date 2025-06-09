package wesleyfrederickoh.wesleyfrederickohUnity.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Player;
import wesleyfrederickoh.wesleyfrederickohUnity.model.PlayerSkill;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Skill;

import java.util.List;
import java.util.UUID;

@Repository
public interface PlayerSkillRepository extends JpaRepository<PlayerSkill, UUID> {
    List<PlayerSkill> findAllByPlayer(Player player);
    // Add any other specific query methods if needed, e.g., findByPlayerAndSkill
}
